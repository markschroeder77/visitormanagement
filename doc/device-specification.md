# Device Interface Specification

> **Version:** 1.0  
> **Date:** May 17, 2026  
> **Project:** VisitorManagement (CleanArchitecture.Blazor)

---

## 1. Overview

This document defines how physical devices (turnstiles, kiosks, QR scanners, tablets, etc.) interface with the VisitorManagement system to perform visitor check-in and check-out operations.

The system is a **Blazor Server** application. Currently, all check-in/check-out flows are driven through the interactive web UI. This specification outlines the architecture, data model, and integration points required for direct device communication.

---

## 2. Domain Model

### 2.1 Device Entity

```csharp
public class Device : AuditableEntity, IHasDomainEvent, IAuditTrial
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? IPAddress { get; set; }
    public string? Parameters { get; set; }
    public string? Status { get; set; }
    public int? CheckinPointId { get; set; }
    public virtual CheckinPoint? CheckinPoint { get; set; }
    public List<DomainEvent> DomainEvents { get; set; } = new();
}
```

| Field | Type | Description |
|---|---|---|
| `Id` | `int` | Primary key |
| `Name` | `string?` | Human-readable device name |
| `IPAddress` | `string?` | Network address of the device |
| `Parameters` | `string?` | JSON or key-value configuration string |
| `Status` | `string?` | Device operational status (e.g., "Online", "Offline", "Error") |
| `CheckinPointId` | `int?` | Foreign key to the associated check-in point |

### 2.2 CheckinPoint Entity

```csharp
public class CheckinPoint : AuditableEntity, IHasDomainEvent, IAuditTrial, IMustHaveTenant
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Device>? Devices { get; set; } = new HashSet<Device>();
    public int? SiteId { get; set; }
    public virtual Site? Site { get; set; }
    public List<DomainEvent> DomainEvents { get; set; } = new();
}
```

| Field | Type | Description |
|---|---|---|
| `Id` | `int` | Primary key |
| `Name` | `string?` | Check-in point name (e.g., "Main Gate", "Lobby Kiosk") |
| `Description` | `string?` | Optional description |
| `SiteId` | `int?` | Foreign key to the site/location |
| `Devices` | `ICollection<Device>` | Devices assigned to this check-in point |

### 2.3 Relationships

```
Site (1) ──── (N) CheckinPoint (1) ──── (N) Device
```

- A **Site** has many **CheckinPoints**
- A **CheckinPoint** has many **Devices**
- A **Device** belongs to exactly one **CheckinPoint**

---

## 3. Visitor Status Lifecycle

```
PendingVisitor
    ↓  (pre-registration completed)
PendingApproval
    ↓  (approved by host/security)
PendingChecking
    ↓  (checked by security)
PendingCheckin
    ↓  [DEVICE CHECK-IN]
PendingConfirm
    ↓  (confirmed by host)
PendingCheckout
    ↓  [DEVICE CHECK-OUT]
PendingFeedback
    ↓  (feedback submitted)
Finished
```

A visitor can also transition to **Canceled** from any pre-check-in state.

### Status Constants

```csharp
public static class VisitorStatus
{
    public static string PendingVisitor   => "Pending Visitor";
    public static string PendingApproval  => "Pending Approval";
    public static string PendingChecking  => "Pending Checking";
    public static string PendingCheckin   => "Pending Check-in";
    public static string PendingConfirm   => "Pending Confirm";
    public static string PendingCheckout  => "Pending Check-out";
    public static string PendingFeedback  => "Pending Feedback";
    public static string Finished         => "Finished";
    public static string Canceled         => "Canceled";
}
```

---

## 4. Check-in / Check-out Flow

### 4.1 Check-in Flow

```
Device                          Server
  │                                │
  │── Scan QR / Enter PassCode ──→│
  │                                │── SearchVisitorAsync(passCode)
  │                                │── Returns VisitorDto (or null)
  │←── Visitor Details ───────────│
  │                                │
  │── Submit Check-in Data ──────→│
  │   {                            │
  │     PassCode,                  │
  │     CheckinPointId,            │
  │     Temperature,               │
  │     Attachments (photos),      │
  │     Comment,                   │
  │     CompanionCheckins[]        │
  │   }                            │
  │                                │── CreateVisitorHistoryCommand
  │                                │   Stage = "Check-in"
  │                                │── Updates Visitor:
  │                                │   • CheckinDate = DateTime.Now
  │                                │   • Status = PendingConfirm
  │                                │── Updates Companions:
  │                                │   • CheckinDateTime = DateTime.Now
  │←── Result (Success/Failure) ──│
```

### 4.2 Check-out Flow

```
Device                          Server
  │                                │
  │── Scan QR / Enter PassCode ──→│
  │                                │── SearchVisitorAsync(passCode)
  │                                │── Returns VisitorDto (or null)
  │←── Visitor Details ───────────│
  │                                │
  │── Submit Check-out Data ─────→│
  │   {                            │
  │     PassCode,                  │
  │     CheckinPointId,            │
  │     Attachments (photos),      │
  │     Comment,                   │
  │     CompanionCheckouts[]       │
  │   }                            │
  │                                │── CreateVisitorHistoryCommand
  │                                │   Stage = "Check-out"
  │                                │── Updates Visitor:
  │                                │   • CheckoutDate = DateTime.Now
  │                                │   • Status = PendingFeedback
  │                                │── Updates Companions:
  │                                │   • CheckoutDateTime = DateTime.Now
  │←── Result (Success/Failure) ──│
```

---

## 5. Integration Options

### 5.1 Option A: REST API Controllers (Recommended)

Build dedicated ASP.NET Core Web API controllers that devices can call over HTTP.

**Proposed Endpoints:**

| Method | Route | Description |
|---|---|---|
| `GET` | `/api/v1/visitors/{passCode}` | Look up visitor by pass code |
| `POST` | `/api/v1/checkin` | Perform visitor check-in |
| `POST` | `/api/v1/checkout` | Perform visitor check-out |
| `GET` | `/api/v1/devices/{id}/status` | Get device status |
| `POST` | `/api/v1/devices/{id}/status` | Update device status |

**Request/Response Example (Check-in):**

```json
POST /api/v1/checkin
{
    "passCode": "ABC123",
    "checkinPointId": 5,
    "temperature": 36.5,
    "comment": "Arrived via main entrance",
    "companionIds": [12, 13]
}

Response 200:
{
    "success": true,
    "visitorId": 42,
    "newStatus": "Pending Confirm",
    "checkinDateTime": "2026-05-17T09:30:00Z"
}
```

**Implementation Notes:**
- Controllers would use `ISender` (MediatR) to dispatch existing commands
- Authentication via API keys or JWT bearer tokens
- Rate limiting and device registration validation recommended

### 5.2 Option B: SignalR Hub Extension

Extend the existing `SignalRHub` to accept device connections directly.

**Existing Hub URL:** `/signalRHub`

**Proposed Hub Methods:**

```csharp
public async Task DeviceCheckin(string deviceId, string deviceSecret, CheckinRequest request)
public async Task DeviceCheckout(string deviceId, string deviceSecret, CheckoutRequest request)
public async Task DeviceHeartbeat(string deviceId, DeviceStatus status)
```

**Implementation Notes:**
- Devices authenticate on connect using device ID + secret
- Real-time bidirectional communication
- Lower latency than REST polling
- Reuses existing SignalR infrastructure

### 5.3 Option C: Kiosk Mode (No-Code Integration)

Run the existing `/visitor/checkin` Blazor page on a tablet or kiosk device in full-screen browser mode.

**Capabilities already built in:**
- Camera-based QR code scanning (JavaScript interop)
- Photo capture and upload
- Check-in point selection dropdown
- Temperature input field
- Companion check-in/check-out toggles
- Comment field

**Requirements:**
- Modern web browser (Chrome, Edge, Safari)
- Camera access for QR scanning
- Internet connectivity to the server

---

## 6. Device Registration & Management

Devices are managed through the Blazor Server UI at the `/visitor/devices` page.

### Device CRUD Operations

| Operation | Description |
|---|---|
| **Create** | Register a new device with name, IP address, parameters, and assigned check-in point |
| **Read** | View device details and status |
| **Update** | Modify device configuration |
| **Delete** | Remove a device from the system |

### Device DTO

```csharp
public class DeviceDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? IPAddress { get; set; }
    public string? Parameters { get; set; }
    public string? Status { get; set; }
    public int? CheckinPointId { get; set; }
    public string? CheckinPoint { get; set; }  // Resolved name
    public string? Site { get; set; }           // Resolved site name
}
```

---

## 7. Security Considerations

| Concern | Recommendation |
|---|---|
| **Device Authentication** | Each device should have a unique secret key stored in `Parameters` or a separate credential store |
| **API Authentication** | Use JWT bearer tokens or API key headers for REST endpoints |
| **Transport Security** | All device communication must use TLS (HTTPS / WSS) |
| **Input Validation** | All device-submitted data must pass through existing `CreateVisitorHistoryCommandValidator` |
| **Device Authorization** | A device should only be able to check in/out visitors at its assigned `CheckinPointId` |
| **Audit Trail** | All check-in/check-out operations are recorded in `VisitorHistory` and `ApprovalHistory` tables |

---

## 8. Error Handling

Devices should handle the following error scenarios:

| HTTP Status / SignalR Error | Meaning | Device Action |
|---|---|---|
| `400 Bad Request` | Validation failure | Display error message, retry with corrected data |
| `401 Unauthorized` | Missing/invalid credentials | Re-authenticate |
| `403 Forbidden` | Device not authorized for this operation | Contact administrator |
| `404 Not Found` | Visitor pass code not found | Prompt for re-scan |
| `409 Conflict` | Visitor not in correct status for operation | Inform operator |
| `500 Internal Server Error` | Server error | Retry with exponential backoff |
| `503 Service Unavailable` | Server temporarily unavailable | Retry after delay |

---

## 9. Configuration Parameters

The `Device.Parameters` field can store device-specific configuration as JSON:

```json
{
    "cameraEnabled": true,
    "temperatureRequired": true,
    "qrScannerEnabled": true,
    "printerEnabled": false,
    "autoSubmit": false,
    "timeoutSeconds": 30,
    "allowedStages": ["Check-in", "Check-out"]
}
```

---

## 10. Future Considerations

- **Offline Mode:** Cache visitor data locally on the device and sync when connectivity is restored
- **Biometric Integration:** Support fingerprint or facial recognition as an alternative to QR codes
- **Printing:** Generate and print visitor badges at the check-in point
- **Webhook Notifications:** Notify external systems when check-in/check-out events occur
- **Device Health Monitoring:** Centralized dashboard for device status, battery levels, and connectivity

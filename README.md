# FilterData

FilterData is an ASP.NET Web Forms Web Application designed for data entry, user role management, list filtering and search, automated record merging, set number updates, and PDF document generation/display.

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Database Configuration & Schema](#database-configuration--schema)
- [Getting Started](#getting-started)
- [License](#license)

---

## Overview

`FilterData` provides a web interface for managing records across different data lists (`lista`, `mergetb`, etc.). It supports user authentication, role management, approval workflows, automated list merging, and PDF exports with uploaded signatures and documents.

---

## Key Features

- **Authentication & User Management**:
  - Login system (`login.aspx`)
  - User signup (`Signup.aspx`)
  - User and access permission management (`ManageUsers.aspx`)

- **Data Entry & Approval**:
  - List data entry (`ListB_Entry.aspx`)
  - Entry approval (`Approve_ListB_Entry.aspx`)
  - Search records by list name (`SerachByListName.aspx`)

- **Automated Data Merging**:
  - Automated merge processing (`AutomationMerge.aspx`)
  - Detailed view of merged records (`ViewMergeDetails.aspx`)

- **Set Number Updates**:
  - Update set numbers (`UpdateSetNo.aspx`)
  - Fill empty set numbers (`UpdateEmptySetNo.aspx`)

- **Document & PDF Processing**:
  - File upload (`upload.aspx`)
  - PDF export and PDF viewer (`exportpdf.aspx`, `DisplayPDF.aspx`)

---

## Tech Stack

- **Framework**: .NET Framework 4.5.2 (ASP.NET Web Forms)
- **Language**: C#
- **Database**: MySQL Server (`MySql.Data.MySqlClient`)
- **Frontend**: HTML5, CSS3, JavaScript / jQuery, Bootstrap, Toastr, Less
- **Master Pages**: `entry.master`, `singlepage.master`

---

## Project Structure

```text
FilterData/
├── App_Code/             # Shared C# classes (ListA.cs, Signup.cs, common.cs)
├── css/                  # Custom and library CSS styles
├── fonts/                # Icon and typography fonts
├── images/               # Image assets
├── img/                  # UI icons and graphics
├── js/                   # Frontend scripts and jQuery modules
├── less/                 # Less stylesheets
├── mergeTmp/             # Temporary folder for merged output PDFs
├── signatureTmp/         # Temporary folder for signature PDF files
├── toastr/               # Toastr notification plugin assets
├── uploadTmp/            # Temporary directory for uploaded files
├── welcomecss/           # Welcome page styling
├── Approve_ListB_Entry.aspx # List entry approval page
├── AutomationMerge.aspx  # Automated record merging tool
├── DisplayPDF.aspx       # Embedded PDF display page
├── ListB_Entry.aspx      # List entry input page
├── ManageUsers.aspx      # User administration page
├── SerachByListName.aspx # Search records by list name
├── Signup.aspx           # User registration page
├── UpdateEmptySetNo.aspx # Batch update empty set numbers
├── UpdateSetNo.aspx      # Set number modification page
├── ViewMergeDetails.aspx # View merge execution details
├── Web.config            # ASP.NET application configuration
├── Web.Debug.config      # Debug environment configuration transform
├── changes.txt           # Database schema migration logs
├── entry.master          # Main application layout master page
├── exportpdf.aspx        # PDF export handler
├── login.aspx            # User login page
├── singlepage.master     # Minimal layout master page
└── upload.aspx           # File upload page
```

---

## Database Configuration & Schema

### Connection String Configuration
The MySQL database connection string is located in `Web.config`:

```xml
<connectionStrings>
  <add name="cn" connectionString="Server=localhost;User ID=dssitwing;Password=YOUR_PASSWORD;Database=FilterData;Pooling=true" providerName="MySql.Data.MySqlClient"/>
</connectionStrings>
```

Update the `Server`, `User ID`, `Password`, and `Database` values according to your local or target database instance.

### Schema Updates
Detailed SQL schema modifications and column additions are tracked in [`changes.txt`](changes.txt). Primary tables include:
- `users`: Stores user credentials, user names, contact info, creation timestamps, and list access permissions (`ListAccess`).
- `lista`: Stores reference entries, list names, and deletion flags (`IsDeleted`).
- `mergetb`: Stores merged record details, form numbers (`FormNo`), set numbers (`SetNo`), creator tracking, and relation info.

---

## Getting Started

### Prerequisites
1. **Windows OS** with IIS Express or IIS installed.
2. **Visual Studio 2015** (or newer) with ASP.NET development workload enabled.
3. **.NET Framework 4.5.2 runtime / SDK**.
4. **MySQL Server** (version 5.6+ / 8.0+ recommended) with MySQL Connector/NET.

### Setup Instructions
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/pankajinsan/FilterData.git
   cd FilterData
   ```

2. **Configure Database**:
   - Create a MySQL database (e.g., `FilterData`).
   - Run initial schema scripts and apply migrations listed in `changes.txt`.
   - Update `Web.config` with your MySQL connection credentials.

3. **Directory Permissions**:
   Ensure the ASP.NET runtime user account has write permissions to the following temporary folders:
   - `signatureTmp/`
   - `uploadTmp/`
   - `mergeTmp/`

4. **Run the Project**:
   - Open the project folder in Visual Studio or configure a web site in IIS.
   - Run the application (`F5` or `Ctrl+F5` in Visual Studio).
   - Navigate to `login.aspx` in your browser.

---

## License

This project is maintained by [pankajinsan](https://github.com/pankajinsan).

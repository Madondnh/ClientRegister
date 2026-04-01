# Product Catalog Management System

## Overview
Product Catalog Management System is a .NET-based solution that exposes a RESTful API for managing products.
The solution is structured using a layered architecture to ensure separation of concerns, testability, and long-term maintainability.

## Prerequisites
- .NET SDK 8.0 or later
- Visual Studio 2022 (recommended) or VS Code
- Git

## Build and Run the Solution
---

## 🛠 Opening and Running the Solution in Visual Studio

If you are using **Visual Studio 2022**, follow these steps to manage the multi-project architecture.

### 1. Open the Solution
1. Launch **Visual Studio**.
2. Select **Open a project or solution**.
3. Navigate to the `ProductCatalogManagementSystem` folder and open the **`.sln`** file.

### 2. Configure Multiple Startup Projects
To ensure both the Backend and Frontend start together:
1. Right-click on the **Solution 'ProductCatalogManagementSystem'** in the Solution Explorer.
2. Select **Configure Startup Projects...**
3. Select the **Multiple startup projects** radio button.
4. Set the following **Actions**:
    * **Presentation\CatalogAPI**: `Start`
    * **Presentation\Catalog.Web**: `Start`
5. Click **Apply** and then **OK**.



### 3. Run the Solution
1. Press **F5** or click the **Start** button (it will now show "Multiple Projects").
2. Visual Studio will:
   * Build the .NET layers (**Infrastructure**, **Application**, **API**).
   * Launch the **Swagger UI** for API testing.
   * Start the **Angular CLI** and open the frontend in your default browser.

### 4. Stopping the Solution
* Click the **Red Stop Button** in the top toolbar to shut down both the API and the Angular development server.

##  🛠 Opening and Running the Solution in Visual Studio Code

Follow these steps to open the **ProductCatalogManagementSystem** and launch both the Backend and Frontend simultaneously.

### 1. Open the Root Folder
1. Launch **Visual Studio Code**.
2. Select **File > Open Folder...**
3. Navigate to and select the **ProductCatalogManagementSystem** directory. 
   *(This is the root folder containing the `Presentation`, `Application`, and `Infrastructure` layers).*

### 2. Prepare Dependencies
Ensure both projects are ready for execution by running these commands in your terminal:

* **Backend:**
* ```bash
    dotnet restore
    ```
* **Frontend:**
* ```bash
    cd Presentation/Catalog.Web
    npm install
    ```

### 3. Select the Launch Configuration
1. Open the **Run and Debug** view by clicking the play icon in the sidebar or pressing `Ctrl+Shift+D`.
2. Locate the dropdown menu at the top of the sidebar.
3. Select the configuration named: **"Both (API & Angular)"**.

### 4. Start the Solution
1. Press the **Green Play Button** (or hit `F5`).
2. **CatalogAPI:** Will build and open the Swagger documentation in your default browser.
3. **Catalog.Web:** Will compile the Angular bundles and launch Microsoft Edge to `http://localhost:61329`.

### 5. Stopping the Solution
To end your session, click the **Red Square (Stop)** button on the debug toolbar. Because this is a **Compound Launch**, it will automatically terminate both the .NET API and the Angular development server at the same time.

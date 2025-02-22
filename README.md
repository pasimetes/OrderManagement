## Prerequisites

Requirements:
- **Visual Studio 2022**: Download and install from the [Visual Studio downloads page](https://visualstudio.microsoft.com/downloads/). During installation, ensure you select the **ASP.NET and web development** workload.
- **.NET 8 SDK**: Download and install from the [.NET downloads page](https://dotnet.microsoft.com/download/dotnet/8.0).
- **SQL Server**: Ensure you have SQL Server installed. You can use SQL Server Express for local development.

Steps to launch:
1. **Clone the Repository:**
   - Open a terminal or command prompt.
   - Clone the repository to your local machine using Git:
     ```bash
     git clone https://github.com/pasimetes/OrderManagement.git

2. **Navigate to the Project Directory:**
    - Change into the project directory:
      ```bash
      cd <project-directory>
    
3. **Restore Dependencies:**
    - Run the following command to restore the necessary packages:
        ```bash
        dotnet restore
        
4. **Configure Connection String:**
    - Open the appsettings.json file in a text editor.
    - Update the connection string for SQL Server:
      ```json
      "ConnectionStrings": {
          "DefaultConnection": "Server=<server-name>;Database=<database-name>;Trusted_Connection=True;TrustServerCertificate=True"
      }
      
5. **Apply Migrations:**
    - Run the folowing command to apply migrations:
      ```bash
      dotnet ef database update
   
6. **Run the Application:**
   - Use the following command to run the application:
      ```bash
      dotnet run
    
8. **Access the API:**
    - Open your browser and navigate to https://localhost:7073/swagger/index.html to access the API.

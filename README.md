## Prerequisites

Requirements:
- **Visual Studio 2022**: Download and install from the [Visual Studio downloads page](https://visualstudio.microsoft.com/downloads/). During installation, ensure you select the **ASP.NET and web development** workload.
- **.NET 8 SDK**: Download and install from the [.NET downloads page](https://dotnet.microsoft.com/download/dotnet/8.0).
- **SQL Server**: Ensure you have SQL Server installed. You can use SQL Server on **Docker** for local development.
- **Docker**: Download and install from the [Docker downloads page](https://docs.docker.com/desktop/setup/install/windows-install/).

Steps to launch:
1. **Clone the Repository:**
   - Open a terminal or command prompt.
   - Clone the repository to your local machine using Git:
     ```bash
     git clone https://github.com/pasimetes/OrderManagement.git

2. **Navigate to the Project Directory:**
    - Change into the project directory:
      ```bash
      cd <project-directory>/OrderManagement

3. **Restore Dependencies:**
    - Run the following command to restore the necessary packages:
        ```bash
        dotnet restore
        
4. **Build Docker Image:**
      - Run the following command to build docker image:
        ```bash
        docker-compose up --build
        
5. **Apply Migrations:**
    - Change into the project directory:
      ```bash
      cd <project-directory>/OrderManagement/OrderManagement.WebApi
    - Run the following command to install EntityFramework CLI
      ```bash
      dotnet tool install --global dotnet-ef --version 9.0.2
    - Run the folowing command to apply migrations:
      ```bash
      dotnet ef database update
    
6. **Access the API:**
    - Open your browser and navigate to https://localhost:8080/swagger/index.html to access the API.

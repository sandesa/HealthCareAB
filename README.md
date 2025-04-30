# HealthCareAB
A fullstack application for a health care organization.

## Specs
- Frontend: React.js
- Backend: ASP.NET Core
- Tests: xUnit

## How to run
### Step 1:
- Clone the repo: `git clone https://github.com/sandesa/HealthCareAB.git`

### Step 2:
- Configurate startup projects:
  - Open solution in Visual Studio
  - Right click the solution and go to `Configure Startup Projects...`
  - Select `Multiple startup projects:`
  - Change `Action` from `None` to `Start` for each project except `XUnit` and `FrontendApp`
  - Run the new startup profile

### Step 3:
- Run the frontend project:
  - `FrontendApp` is a React app which means you need to have `Node.js` installed
  - Open powershell and type `node -v` & `npm -v` to check that everything is installed
  - If not, go to the website `nodejs.org` to download node
  - When node is installed open powershell and navigate to the `FrontendApp` directory
  - Type `npm run dev` to run the app and go to `localhost:3000` 

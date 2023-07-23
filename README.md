# EcoSimulator

EcoSimulator is a fun and educational project that simulates a small-scale ecosystem right in your browser. The application allows users to design their own habitats, place various animal and plant species, and watch as they interact, grow, and evolve over time.

## Tech Stack

- **Frontend:** TypeScript, React, and Vite for a modern and efficient web application.
- **Backend:** ASP.NET for a robust and structured API.
- **Simulation Microservice:** Python, providing the power and flexibility for complex computations.

## Local Setup

Here's how to get the project running on your local machine for development and testing purposes:

### Prerequisites

- Node.js and npm (Node Package Manager)
- .NET 5.0 or later
- Python 3.8 or later

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/mistahoward/ecosimulator.git
   ```
2. Install dependencies for the frontend:
   ```bash
   cd ecosimulator/frontend
   npm install
   ```
3. Run the front end:
   ```bash
   npm run dev
   ```
4. In a new terminal, navigate to the backend directory and run the backend:
  ```bash
  cd ../backend
  dotnet run
  ```
5. In another new terminal, navigate to the microservice directory, activate the virtual environment, and run the microservice:
   ```bash
   cd ../microservice
   source venv/bin/activate
   python3 main.py
   ```
You should now have the frontend running at localhost:3000, the backend at localhost:5000, and the Python microservice running in its own terminal.

## Contributing

Feel free to submit pull requests to contribute. You can also report issues or ask questions by creating an issue in the repository.

## License

This project is licensed under the MIT License.

## Acknowledgements

Project inspired by a desire to combine a fun, educational experience with showcasing diverse technology skills.

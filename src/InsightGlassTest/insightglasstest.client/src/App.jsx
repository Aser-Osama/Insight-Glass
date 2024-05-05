import { useEffect, useState } from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";

function App() {
  const [forecasts, setForecasts] = useState();
  const [updateData, setUpdateData] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  useEffect(() => {
    populateWeatherData();
  }, [updateData]);

  const contents =
    forecasts === undefined ? (
      <p>
        <em>
          Loading... Please refresh once the ASP.NET backend has started. See
          <a href="https://aka.ms/jspsintegrationreact">
            https://aka.ms/jspsintegrationreact
          </a>
          for more details.
        </em>
      </p>
    ) : (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map((forecast) => (
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );

  const updateButton = (
    <button className="btn btn-danger" onClick={() => setUpdateData((s) => !s)}>
      Update Data
    </button>
  );

  return (
    <>
      {!isLoading && (
        <div className="container p-5">
          <h1 id="tableLabel">Weather forecast</h1>
          <p>This component demonstrates fetching data from the server.</p>
          {contents}
          <div className="text-center">{updateButton}</div>
        </div>
      )}
      {isLoading && <span> Loading... </span>}
    </>
  );

  async function populateWeatherData() {
    setIsLoading(true);
    const response = await fetch("weatherforecast");
    const data = await response.json();
    setForecasts(data);
    setIsLoading(false);
  }
}

export default App;

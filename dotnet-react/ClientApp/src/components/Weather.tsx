import React from "react";

type Forecast = {
  date: string;
  temperatureC: string;
  temperatureF: string;
  summary: string;
};

const Weather: React.FC = () => {
  const [forecasts, setForecasts] = React.useState<Forecast[]>([]);

  React.useEffect(() => {
    const fetchData = async () => {
      const response = await fetch(
        window.configuration.backend + "weatherforecast"
      );
      const data: Forecast[] = await response.json();
      setForecasts(data);
    };

    fetchData();
  }, []);
  return (
    <>
      {forecasts.map((forecast, i) => (
        <p key={i}>
          {forecast.temperatureC}°C {forecast.summary} {forecast.date}
        </p>
      ))}
    </>
  );
};

export default Weather;

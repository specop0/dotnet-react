import React from "react";
import { useGetWeatherForecast } from "../openapi/backendComponents";

const Weather: React.FC = () => {
  const { data, isLoading, error } = useGetWeatherForecast({});
  const forecasts = data ?? [];

  if (isLoading) {
    return <div role="progressbar">Loading...</div>;
  }

  if (error) {
    return <>Error: {JSON.stringify(error)}</>;
  }

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

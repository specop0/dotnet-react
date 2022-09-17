import React from "react";
import { CircularProgress, Typography } from "@mui/material";
import { useGetWeatherForecast } from "../openapi/backendComponents";

const Weather: React.FC = () => {
  const { data, isLoading, error } = useGetWeatherForecast({});
  const forecasts = data ?? [];

  if (isLoading) {
    return <CircularProgress />;
  }

  if (error) {
    return <>Error: {JSON.stringify(error)}</>;
  }

  return (
    <>
      {forecasts.map((forecast, i) => (
        <Typography key={i}>
          {forecast.temperatureC}°C {forecast.summary} {forecast.date}
        </Typography>
      ))}
    </>
  );
};

export default Weather;

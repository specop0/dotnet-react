import { RequestHandler, rest } from "msw";
import { GetWeatherForecastResponse } from "../openapi/backendComponents";
import { WeatherForecastMock } from "./mocks";

export const handlerPath = {
  getWeatherForecast: "*/api/WeatherForecast" as const,
};

export const createGetWeatherForecastHandler = (
  response: GetWeatherForecastResponse
): RequestHandler => {
  return rest.get(handlerPath.getWeatherForecast, (req, res, ctx) => {
    return res(ctx.status(200), ctx.json<GetWeatherForecastResponse>(response));
  });
};

export const handlers = [
  createGetWeatherForecastHandler(WeatherForecastMock.buildList(3)),
];

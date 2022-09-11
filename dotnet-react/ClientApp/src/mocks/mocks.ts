import * as Factory from "factory.ts";
import { faker } from "@faker-js/faker";
import { WeatherForecast } from "../openapi/backendSchemas";

export const WeatherForecastMock = Factory.Sync.makeFactory<WeatherForecast>({
  date: Factory.each(() => faker.date.future().toISOString()),
  summary: Factory.each(() => faker.lorem.sentence()),
  temperatureC: Factory.each(() => faker.datatype.number()),
  temperatureF: Factory.each(() => faker.datatype.number()),
});

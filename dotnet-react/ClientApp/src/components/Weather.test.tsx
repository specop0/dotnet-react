import React from "react";
import { screen, waitForElementToBeRemoved } from "@testing-library/react";
import { faker } from "@faker-js/faker";
import { rest } from "msw";
import Weather from "./Weather";
import { render } from "../utils/render";
import { WeatherForecastMock } from "../mocks/mocks";
import {
  createGetWeatherForecastHandler,
  handlerPath,
} from "../mocks/handlers";
import { GetWeatherForecastError } from "../openapi/backendComponents";

describe("<Weather />", () => {
  it("should display weather", async () => {
    // Arrange
    const weatherForecasts = WeatherForecastMock.buildList(2);
    const getWeatherForecastHandler =
      createGetWeatherForecastHandler(weatherForecasts);

    const expectedWeatherForecastA = `${weatherForecasts[0].temperatureC}°C ${weatherForecasts[0].summary} ${weatherForecasts[0].date}`;
    const expectedWeatherForecastB = `${weatherForecasts[1].temperatureC}°C ${weatherForecasts[1].summary} ${weatherForecasts[1].date}`;

    // Act
    render(<Weather />, { mocks: [getWeatherForecastHandler] });
    const progressbar = screen.getByRole("progressbar");
    await waitForElementToBeRemoved(progressbar);

    // Assert
    expect(screen.getByText(expectedWeatherForecastA)).toBeInTheDocument();
    expect(screen.getByText(expectedWeatherForecastB)).toBeInTheDocument();
  });

  it("should display error", async () => {
    // Arrange
    const error: GetWeatherForecastError = {
      status: 400,
      payload: {
        error: {
          code: "400",
          message: faker.lorem.sentence(),
        },
      },
    };
    const getWeatherForecastHandler = rest.get(
      handlerPath.getWeatherForecast,
      (req, res, ctx) => {
        return res(ctx.status(400), ctx.json(error));
      }
    );

    const expectedMessage = `Error: ${JSON.stringify(error)}`;

    // Act
    render(<Weather />, { mocks: [getWeatherForecastHandler] });
    const progressbar = screen.getByRole("progressbar");
    await waitForElementToBeRemoved(progressbar);

    // Assert
    expect(screen.getByText(expectedMessage)).toBeInTheDocument();
  });
});

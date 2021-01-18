import React from "react";
import { unmountComponentAtNode, render } from "react-dom";
import { act, Simulate } from "react-dom/test-utils";
import { StockTicker } from "./StockTicker";

let container = null;

class mockSignalR {
  invoke() {}
  onReceiveMessage() {}
}
beforeEach(() => {
  // setup a DOM element as a render target
  container = document.createElement("div");
  document.body.appendChild(container);
});

afterEach(() => {
  // cleanup on exiting
  unmountComponentAtNode(container);
  container.remove();
  container = null;
});

it("displays correctly with no information", () => {
  act(() => {
    render(<StockTicker signalR={new mockSignalR()}></StockTicker>, container);
    expect(container.querySelector(".quoteHeader").textContent).toBe(
      "Enter your username"
    );
    expect(container.querySelector(".noStocksMessage").textContent).toBe(
      "Enter stock symbols in the dashboard"
    );
  });
});

it("displays correctly with information", () => {
  act(() => {
    render(<StockTicker signalR={new mockSignalR()}></StockTicker>, container);
    const userInput = container.querySelector(".userInput");
    const userButton = container.querySelector(".userButton");
    const stockButton = container.querySelector(".stockButton");
    const stockInput = container.querySelector(".stockInput");

    Simulate.change(userInput, "jon");
    Simulate.click(userButton);
    Simulate.change(stockInput);
    console.log(userInput.value);
    expect(container.querySelector(".quoteHeader").textContent).toBe(
      "jon's stocks"
    );
    expect(container.querySelector(".noStocksMessage")).toBeFalsey();
  });
});

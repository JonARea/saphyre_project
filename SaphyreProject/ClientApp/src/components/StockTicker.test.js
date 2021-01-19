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
      "Enter your username and stock symbols"
    );
    expect(container.querySelector(".noStocksMessage").textContent).toBe(
      "No stocks added"
    );
  });
});

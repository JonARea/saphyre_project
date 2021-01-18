import React from "react";
import ReactDOM from "react-dom";
import { MemoryRouter } from "react-router-dom";
import { App } from "./App";
import { unmountComponentAtNode, render } from "react-dom";
import { act } from "react-dom/test-utils";

class mockSignalR {
  invoke() {}
  onReceiveMessage() {}
}
let container = null;
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

it("renders without crashing", () => {
  act(() => {
    render(
      <MemoryRouter basename="">
        <App signalR={new mockSignalR()} />
      </MemoryRouter>,
      container
    );
  });
});

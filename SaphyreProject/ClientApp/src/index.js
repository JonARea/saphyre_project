import "bootstrap/dist/css/bootstrap.css";
import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import { App } from "./App";
import registerServiceWorker from "./registerServiceWorker";
import { SignalR } from "./connection";

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const rootElement = document.getElementById("root");
const signalR = new SignalR();

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <App signalR={signalR} />
  </BrowserRouter>,
  rootElement
);

registerServiceWorker();

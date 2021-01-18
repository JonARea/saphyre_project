import React from "react";
import { Route } from "react-router";
import { StockTicker } from "./components/StockTicker";
import "./custom.css";

export const App = (props) => (
  <Route exact path="/">
    <StockTicker signalR={props.signalR} />
  </Route>
);

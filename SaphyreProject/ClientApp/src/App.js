import React, { Component } from "react";
import { Route } from "react-router";
import { StockTicker } from "./components/StockTicker";

import "./custom.css";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return <Route exact path="/" component={StockTicker} />;
  }
}

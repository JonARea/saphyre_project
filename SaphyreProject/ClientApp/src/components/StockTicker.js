import React, { Component } from "react";
import { Dashboard } from "./Dashboard";
import { QuoteTable } from "./QuoteTable";
import { SignalR } from "../connection";
export class StockTicker extends Component {
  static displayName = StockTicker.name;

  constructor(props) {
    super(props);
    this.state = {
      user: "",
      stocks: [],
      signalR: null,
      updateInterval: null,
    };
  }

  setUser(user) {
    this.setState({
      user,
    });
  }

  updateStocks(stocks) {
    this.setState({
      stocks,
    });
  }

  addStock(symbol) {
    this.signalR.invoke("AddStock", this.state.user, symbol);
  }

  componentDidMount() {
    const signalR = new SignalR();
    signalR.onReceiveStocks((stocks) => this.updateStocks.bind(this, stocks));

    const updateInterval = setInterval(() => {
      signalR.invoke("GetMyStocks", this.state.user);
    }, 5000);

    this.setState({
      signalR,
      updateInterval,
    });
  }

  componentWillUnmount() {
    this.state.updateInterval.clear();
  }

  render() {
    return (
      <div>
        <Dashboard
          setUser={(user) => this.setUser.bind(this, user)}
          addStock={(stock) => this.addStock.bind(this, stock)}
        />
        <QuoteTable />
      </div>
    );
  }
}

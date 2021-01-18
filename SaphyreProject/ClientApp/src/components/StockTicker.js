import React, { Component } from "react";
import { Dashboard } from "./Dashboard";
import { QuoteTable } from "./QuoteTable";
export class StockTicker extends Component {
  static displayName = StockTicker.name;

  constructor(props) {
    super(props);
    this.state = {
      user: "",
      stocks: [],
      signalR: null,
      updateInterval: null,
      showDashboard: true,
    };

    this.setUser = this.setUser.bind(this);
    this.addStock = this.addStock.bind(this);
    this.updateStocks = this.updateStocks.bind(this);
  }

  setUser(user) {
    this.setState({
      user,
    });
  }

  updateStocks(stocks) {
    if (stocks) {
      this.setState({
        stocks,
      });
    }
  }

  addStock(symbol) {
    this.state.signalR
      .invoke("AddStock", this.state.user, symbol)
      .then((res) => {
        console.log(res);
      });
  }

  initializeSignalR() {
    const signalR = this.props.signalR;
    signalR.onReceiveMessage("ReceiveStocks", this.updateStocks);
    signalR.onReceiveMessage("AddStockResult", (result) => console.log(result));
    const updateInterval = setInterval(() => {
      signalR
        .invoke("GetMyStocks", this.state.user)
        .then(this.updateStocks)
        .catch((err) => console.error(err));
    }, 5000);

    this.setState({
      signalR,
      updateInterval,
    });
  }

  componentDidMount() {
    this.initializeSignalR();
  }

  componentWillUnmount() {
    clearInterval(this.state.updateInterval);
  }

  toggleDashboard(show) {
    this.setState({
      showDashboard: show,
    });
  }

  render() {
    return (
      <div className="stockTicker">
        <Dashboard
          setUser={this.setUser}
          addStock={this.addStock}
          visible={this.state.showDashboard}
          disableAddStock={!this.state.user}
        />
        <div className="pusher">
          <h2 className="quoteHeader">
            {this.state.user
              ? this.state.user + "'s stocks"
              : "Enter your username"}
          </h2>
          <QuoteTable stocks={this.state.stocks} />
        </div>
      </div>
    );
  }
}

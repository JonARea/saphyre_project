import * as signalR from "@microsoft/signalr";

export class SignalR {
  _connection;
  get connection() {
    return this._connection;
  }

  constructor() {
    this._connection = new signalR.HubConnectionBuilder()
      .withUrl("/stockquotehub")
      .build();

    this.connection
      .start()
      .then()
      .catch((err) => console.error(err.toString()));
  }

  onReceiveStocks(func) {
    this._connection.on("ReceiveStocks", func());
  }

  invoke(method, ...args) {
    this.connection
      .invoke(method, ...args)
      .then((res) => console.log("invoked", res))
      .catch((err) => console.error(err.toString()));
  }
}

import * as signalR from "@microsoft/signalr";

export class SignalR {
  _connection;

  constructor() {
    this._connection = new signalR.HubConnectionBuilder()
      .withUrl("/stockquotehub")
      .build();

    this._connection
      .start()
      .then()
      .catch((err) => console.error(err.toString()));
  }

  onReceiveMessage(message, func) {
    this._connection.on(message, () => func());
  }

  invoke(method, ...args) {
    return this._connection.invoke(method, ...args);
  }
}

import React, { useState } from "react";

export const Dashboard = (props) => {
  const [userName, setUserName] = useState("");
  const [stockSymbol, setStockSymbol] = useState("");
  const handleChange = (event, type) => {
    if (type === "userName") {
      setUserName(event.target.value);
    } else if (type === "stock") {
      setStockSymbol(event.target.value);
    }
  };
  const handleSubmit = (event, type) => {
    if (type === "userName") {
      props.setUser(userName);
      setUserName("");
    } else if (type === "stock") {
      console.log(props, stockSymbol);
      props.addStock(stockSymbol);
      setStockSymbol("");
    }

    event.preventDefault();
  };

  let visible = props.visible ? "visible" : "";

  return (
    <div className={"dashboard ui vertical sidebar inverted " + visible}>
      <div>
        <h3>Dashboard</h3>
      </div>
      <div className="ui form inverted">
        <div className="field">
          <label>Username</label>
          <input
            type="text"
            placeholder="Username"
            value={userName}
            className="userInput"
            onChange={(event) => handleChange(event, "userName")}
          />

          <button
            className="ui button userButton"
            onClick={(event) => handleSubmit(event, "userName")}
          >
            Submit
          </button>
        </div>
        <div className="field">
          <label>Add Stock</label>
          <input
            type="text"
            placeholder="e.g. MSFT"
            value={stockSymbol}
            className="stockInput"
            onChange={(event) => handleChange(event, "stock")}
          />
          <button
            disabled={props.disableAddStock}
            className="ui button stockButton"
            onClick={(event) => handleSubmit(event, "stock")}
          >
            Add
          </button>
        </div>
      </div>
    </div>
  );
};

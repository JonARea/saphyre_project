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
      props.addStock(stockSymbol);
      setStockSymbol("");
    }

    event.preventDefault();
  };

  return (
    <div className="container">
      <div className="row">&nbsp;</div>
      <div className="row">
        <div className="col-2">Enter your username</div>
        <div className="col-4">
          <input
            type="text"
            value={userName}
            id="userInput"
            onChange={(event) => handleChange(event, "userName")}
          />
        </div>
        <input
          type="button"
          value="Submit"
          onClick={(event) => handleSubmit(event, "userName")}
        />
      </div>
      <div className="row">
        <div className="col-2">Stock Symbol to Add</div>
        <div className="col-4">
          <input
            type="text"
            value={stockSymbol}
            id="stockInput"
            onChange={(event) => handleChange(event, "stock")}
          />
        </div>
        <div className="col-6">
          <input
            type="button"
            id="sendButton"
            value="Add Stock"
            onClick={(event) => handleSubmit(event, "stock")}
          />
        </div>
      </div>
    </div>
  );
};

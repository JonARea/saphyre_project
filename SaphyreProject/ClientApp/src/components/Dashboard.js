import React, { useState } from "react";
import { Button, Divider, Form, Header } from "semantic-ui-react";

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
    <div className="dashboard">
      <div>
        <Header as="h2" inverted>
          StockTicker
        </Header>
        <Divider inverted></Divider>
      </div>
      <Form inverted={true}>
        <Form.Field>
          <label>Username</label>
          <input
            type="text"
            placeholder="Username"
            value={userName}
            className="userInput"
            onChange={(event) => handleChange(event, "userName")}
          />
          <Button
            className="ui button userButton"
            onClick={(event) => handleSubmit(event, "userName")}
          >
            Submit
          </Button>
        </Form.Field>
        <Form.Field>
          <label>Add Stock</label>
          <input
            type="text"
            placeholder="e.g. MSFT"
            value={stockSymbol}
            className="stockInput"
            onChange={(event) => handleChange(event, "stock")}
          />
          <Button
            disabled={props.disableAddStock}
            className="ui button stockButton"
            onClick={(event) => handleSubmit(event, "stock")}
          >
            Add
          </Button>
        </Form.Field>
      </Form>
    </div>
  );
};

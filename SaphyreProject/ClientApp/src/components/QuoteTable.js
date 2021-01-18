import React from "react";

export const QuoteTable = (props) => {
  const { stocks } = props;

  return stocks.length ? (
    <table className="ui celled table">
      <thead>
        <tr>
          <th>Stock</th>
          <th>Price</th>
        </tr>
      </thead>
      <tbody>
        {stocks &&
          stocks.map((stock) => (
            <tr key={stock.toString()}>
              <td>{stock.symbol}</td>
              <td>{stock.price || "Price Pending"}</td>
            </tr>
          ))}
      </tbody>
    </table>
  ) : (
    <h4 className="noStocksMessage">Enter stock symbols in the dashboard</h4>
  );
};

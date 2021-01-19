import React from "react";
import { Table } from "semantic-ui-react";

export const QuoteTable = (props) => {
  const { stocks } = props;

  return stocks?.length ? (
    <Table className="table">
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell>Stock</Table.HeaderCell>
          <Table.HeaderCell>Price</Table.HeaderCell>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {stocks &&
          stocks.map((stock) => (
            <Table.Row key={stock.toString()}>
              <Table.Cell>{stock.symbol}</Table.Cell>
              <Table.Cell>{stock.price || "Price Pending"}</Table.Cell>
            </Table.Row>
          ))}
      </Table.Body>
    </Table>
  ) : (
    <h4 className="noStocksMessage">No stocks added</h4>
  );
};

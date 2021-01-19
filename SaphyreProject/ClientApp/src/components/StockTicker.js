import React, { useEffect, useState } from "react";
import { Sidebar } from "semantic-ui-react";
import { AddStockModal } from "./AddStockResult";
import { Dashboard } from "./Dashboard";
import { QuoteTable } from "./QuoteTable";

export const StockTicker = (props) => {
  // State
  const { signalR } = props;
  const [user, setUser] = useState("");
  const [stocks, setStocks] = useState([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [modalContent, setModalContent] = useState("");

  const addStock = (symbol) => {
    signalR.invoke("AddStock", user, symbol).then((message) => {
      setModalContent(message);
      setModalOpen(true);
    });
  };

  useEffect(() => {
    console.log(signalR);
    const fetchStocks = () => {
      signalR
        .invoke("GetMyStocks", user)
        .then((stocks) => {
          if (stocks?.length) {
            setStocks(stocks);
          }
        })
        .catch((err) => console.error(err));
    };
    const updateInterval = setInterval(fetchStocks, 3000);

    return () => clearInterval(updateInterval);
  }, [signalR, user]);

  return (
    <Sidebar.Pushable className="stockTicker">
      <Sidebar inverted="true" vertical="true" visible>
        <Dashboard
          setUser={(user) => {
            setUser(user);
            setStocks(null);
          }}
          addStock={addStock}
          disableAddStock={!user}
        />
      </Sidebar>
      <Sidebar.Pusher>
        <h2 className="quoteHeader">
          {user ? user + "'s stocks" : "Enter your username and stock symbols"}
        </h2>
        <QuoteTable stocks={stocks} />
      </Sidebar.Pusher>
      <AddStockModal
        open={modalOpen}
        content={modalContent}
        closeModal={() => setModalOpen(false)}
      ></AddStockModal>
    </Sidebar.Pushable>
  );
};

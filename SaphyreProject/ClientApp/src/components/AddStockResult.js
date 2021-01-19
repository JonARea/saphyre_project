import React from "react";
import { Modal, Button } from "semantic-ui-react";

export const AddStockModal = (props) => (
  <Modal open={props.open} size="mini">
    <Modal.Content>
      <p>{props.content}</p>
    </Modal.Content>
    <Modal.Actions>
      <Button color="green" inverted onClick={props.closeModal}>
        OK
      </Button>
    </Modal.Actions>
  </Modal>
);

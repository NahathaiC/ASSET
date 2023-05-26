import agent from "../../app/api/agent";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import PRList from "./PRList";
import { useState, useEffect } from "react";

export default function Catalog() {
  const [purchaserequisitions, setPRs] = useState<PurchaseRequisition[]>([]);

  useEffect(() => {
    agent.Catalog.list().then(purchaserequisitions => setPRs(purchaserequisitions))
  }, []);

  return (
    <>
      <PRList purchaserequisitions={purchaserequisitions} />
    </>
  );
}

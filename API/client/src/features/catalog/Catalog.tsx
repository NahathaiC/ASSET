import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import PRList from "./PRList";
import { useState, useEffect } from "react";

export default function Catalog() {
  const [purchaserequisitions, setPRs] = useState<PurchaseRequisition[]>([]);

  useEffect(() => {
    fetch("http://localhost:5050/api/PR")
      .then((response) => response.json())
      .then((data) => setPRs(data));
  }, []);

  return (
    <>
      <PRList purchaserequisitions={purchaserequisitions} />
    </>
  );
}

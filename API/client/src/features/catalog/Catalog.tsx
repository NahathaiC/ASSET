import agent from "../../app/api/agent";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import PRList from "./PRList";
import { useState, useEffect } from "react";

export default function Catalog() {
  const [purchaserequisitions, setPRs] = useState<PurchaseRequisition[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    agent.Catalog.list()
    .then(purchaserequisitions => setPRs(purchaserequisitions))
    .catch(error => console.log(error))
    .finally(() => setLoading(false))
  }, [])

  if (loading) return <LoadingComponent message='Loading Purchase requisitions...'/>

  return (
    <>
      <PRList purchaserequisitions={purchaserequisitions} />
    </>
  );
}

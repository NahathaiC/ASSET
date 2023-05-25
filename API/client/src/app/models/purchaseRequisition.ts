export interface PurchaseRequisition {
  id: number;
  title: string;
  requestUser: string;
//   fixHistory: string;
  createDate: string;
  department: string;
  section: string;
  useDate: string;
  prodDesc: string;
  model: string;
  quantity: number;
  unitPrice: number;
  remark?: string;
  status: string;
}

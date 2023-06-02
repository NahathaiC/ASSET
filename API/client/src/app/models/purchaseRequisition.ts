import { Quotation } from "./quotation";

export interface PurchaseRequisition {
  id: number;
  title: string;
  requestUser: string;
  createDate: string;
  department: string;
  section: string;
  useDate: string;
  prodDesc: string;
  model: string;
  quantity: number;
  unitPrice: number;
  remark?: any;
  status: string;
  prPicture: string;

  approverName1: string
  approverName2: string

  quotation: Quotation
}

export interface prParams {
  orderBy: string;
  seachTem?: string;
  department: string[];
  section: string[];
  pageNumber: number;
  pageSize: number;
}

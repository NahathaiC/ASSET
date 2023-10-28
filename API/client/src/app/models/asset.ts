export interface Asset {
    id: string
    no: number
    name: string
    type: string
    serailNo: string;
    owner: Owner
    manufacturer: string
    model: string
    assetStatus: string
    stock: Stock
    assetPic: string
    personInCharge: PersonInCharge;
  }
  
  export interface Owner {
    id: number
    ownerDesc: string
  }
  
  export interface Stock {
    id: number
    type: string
  }

  export interface PersonInCharge { // New interface for personInCharge
    id: number;
    email: string;
    userName: string;
    position: string;
    department: string;
    section: string;
    phone: string;
    status: string;
  }
  
  export interface AssetParams {
    orderBy: string;
    searchTerm?: string;
    pageNumber: number;
    pageSize: number;
  }
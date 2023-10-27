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
    PersonInCharge: string
  }
  
  export interface Owner {
    id: number
    ownerDesc: string
  }
  
  export interface Stock {
    id: number
    type: string
  }
  
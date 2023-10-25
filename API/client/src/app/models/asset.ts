export interface Asset {
    id: string
    no: number
    name: string
    type: string
    owner: Owner
    manufacturer: string
    model: string
    assetStatus: string
    stock: Stock
    assetPic: string
  }
  
  export interface Owner {
    id: number
    ownerDesc: string
  }
  
  export interface Stock {
    id: number
    type: string
  }
  
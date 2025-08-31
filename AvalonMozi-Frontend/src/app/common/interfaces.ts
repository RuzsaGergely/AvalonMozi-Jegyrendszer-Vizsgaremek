export interface CartItem {
    movieTechnicalId: string,
    dateTechnicalId: string,
    movieReadable: string,
    dateReadable: string,
    qty: number,
    uid: number,
    ticketprice: number
}

export interface Cart {
    priceWithoutVat: number,
    priceWithVat: number,
    cartItems: CartItem[]
}
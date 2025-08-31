export interface CartItem {
    movieTechnicalId: string,
    dateTechnicalId: string,
    movieReadable: string,
    dateReadable: string,
    qty: number
}

export interface Cart {
    priceWithoutVat: number,
    priceWithVat: number,
    cartItems: CartItem[]
}
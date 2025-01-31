export interface Account {
    id: string;
    name: string;
    balance: number;
    isFrozen: boolean;
    accountType: string;
    interestRate?: number;
    overdraftLimit?: number;
}

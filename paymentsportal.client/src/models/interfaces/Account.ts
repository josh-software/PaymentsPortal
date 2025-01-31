import { AccountType } from "../enums/AccountType";

export interface Account {
    id?: string;
    name: string;
    balance: number;
    isFrozen: boolean;
    accountType: AccountType;
    interestRate?: number;
    overdraftLimit?: number;
}

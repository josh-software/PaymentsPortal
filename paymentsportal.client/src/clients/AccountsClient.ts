import { Account } from "../models/interfaces/Account";

export class AccountsClient {
    private readonly maxRetries: number = 5;
    private readonly initialDelay: number = 1000;

    private async fetch<T>(url: string): Promise<T> {
        let attempts = 0;
        let delay = this.initialDelay;

        while (attempts < this.maxRetries) {
            try {
                const response = await fetch(url);
                if (!response.ok) {
                    throw new Error(`Error: ${response.status} ${response.statusText}`);
                }
                return await response.json();
            } catch (error) {
                attempts++;
                console.error(`Attempt ${attempts} failed. Retrying in ${delay} ms`, error);
                if (attempts >= this.maxRetries) {
                    console.error("Max retries reached. Failed to fetch data.", error);
                    throw error;
                }
                await this.sleep(delay);
                delay *= 2;
            }
        }
        throw new Error("Failed to fetch data after multiple attempts.");
    }

    private sleep(ms: number) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    async getAccounts(): Promise<Account[]> {
        const url = `accounts`;
        return this.fetch<Account[]>(url);
    }

    async getAccountById(id: string): Promise<Account> {
        const url = `accounts/${id}`;
        return this.fetch<Account>(url);
    }

    //TODO: Handle errors in the POST request
    async createAccount(account: Account): Promise<void> {
        const url = `accounts`;
        await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(account)
        });
    }
}
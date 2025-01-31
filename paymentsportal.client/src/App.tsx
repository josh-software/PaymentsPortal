import { useEffect, useState } from 'react';
import './App.css';
import { AccountsClient } from './clients/AccountsClient';
import { Account } from './models/interfaces/Account';
import { toMoney } from './formatters/formatters';
import { Button } from '@mui/material';
import CreateAccountModal from './components/modals/CreateAccountModal';

function App() {
    const [accounts, setAccounts] = useState<Account[]>();
    const [showCreateAccountModal, setShowCreateAccountModal] = useState<boolean>(false);

    useEffect(() => {
        populateAccountData();
    }, []);

    const contents = accounts === undefined
        ? <p><em>Loading...</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Balance</th>
                    <th>Is Frozen</th>
                    <th>Account Type</th>
                </tr>
            </thead>
            <tbody>
                {accounts.map(account =>
                    <tr key={account.id}>
                        <td>{account.name}</td>
                        <td>{toMoney(account.balance)}</td>
                        <td>{account.isFrozen ? String.fromCodePoint(0x1F9CA) : String.fromCodePoint(0x1F525)}</td>
                        <td>{account.accountType}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Accounts</h1>
            <Button onClick={() => setShowCreateAccountModal(true)}>Create Account</Button>
            {showCreateAccountModal && <CreateAccountModal
                open={showCreateAccountModal}
                onCreate={async (account) => {
                    const accountsClient = new AccountsClient();
                    await accountsClient.createAccount(account);
                    populateAccountData();
                    setShowCreateAccountModal(false);
                }}
                onClose={() => {
                    populateAccountData();
                    setShowCreateAccountModal(false);
                }}
            />}
            {contents}
        </div>
    );

    async function populateAccountData() {
        const accountsClient = new AccountsClient();
        const response = await accountsClient.getAccounts();
        setAccounts(response);
    }
}

export default App;
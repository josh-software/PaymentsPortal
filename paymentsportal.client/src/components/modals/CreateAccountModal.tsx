import React, { useState } from "react";
import { Dialog, DialogTitle, DialogContent, DialogActions, TextField, Button, MenuItem, FormControlLabel, Checkbox } from "@mui/material";
import { Account } from "../../models/interfaces/Account";
import { AccountType } from "../../models/enums/AccountType";

interface CreateAccountModalProps {
    open: boolean;
    onClose: () => void;
    onCreate: (account: Account) => void;
}

const CreateAccountModal: React.FC<CreateAccountModalProps> = ({ open, onClose, onCreate }) => {
    const [name, setName] = useState("");
    const [balance, setBalance] = useState(0);
    const [accountType, setAccountType] = useState<AccountType>(AccountType.Savings);
    const [isFrozen, setIsFrozen] = useState(false);
    const [interestRate, setInterestRate] = useState<number | undefined>(undefined);
    const [overdraftLimit, setOverdraftLimit] = useState<number | undefined>(undefined);

    const handleSubmit = () => {
        const newAccount: Account = {
            name,
            balance,
            isFrozen,
            accountType,
            interestRate: accountType === AccountType.Savings ? interestRate : undefined,
            overdraftLimit: accountType === AccountType.Current ? overdraftLimit : undefined,
        };
        onCreate(newAccount);
        onClose();
    };

    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>Create Account</DialogTitle>
            <DialogContent>
                <TextField label="Name" fullWidth margin="dense" value={name} onChange={(e) => setName(e.target.value)} />
                <TextField label="Balance" fullWidth margin="dense" type="number" value={balance} onChange={(e) => setBalance(parseFloat(e.target.value))} />

                <TextField
                    select
                    label="Account Type"
                    fullWidth
                    margin="dense"
                    value={accountType}
                    onChange={(e) => setAccountType(e.target.value as AccountType)}
                >
                    <MenuItem value={AccountType.Savings}>Savings</MenuItem>
                    <MenuItem value={AccountType.Current}>Current</MenuItem>
                </TextField>

                {accountType === AccountType.Savings && (
                    <TextField
                        label="Interest Rate (%)"
                        fullWidth
                        margin="dense"
                        type="number"
                        value={interestRate ?? ""}
                        onChange={(e) => setInterestRate(parseFloat(e.target.value))}
                    />
                )}

                {accountType === AccountType.Current && (
                    <TextField
                        label="Overdraft Limit"
                        fullWidth
                        margin="dense"
                        type="number"
                        value={overdraftLimit ?? ""}
                        onChange={(e) => setOverdraftLimit(parseFloat(e.target.value))}
                    />
                )}

                <FormControlLabel control={<Checkbox checked={isFrozen} onChange={(e) => setIsFrozen(e.target.checked)} />} label="Freeze Account" />
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose} color="secondary">Cancel</Button>
                <Button onClick={handleSubmit} color="primary" variant="contained">Create</Button>
            </DialogActions>
        </Dialog>
    );
};

export default CreateAccountModal;

export const toMoney = (value: number): string => {
    if (isNaN(value) || value == null) return '£0.00';
    return `${"\u00A3"}${value.toFixed(2)}`;
};

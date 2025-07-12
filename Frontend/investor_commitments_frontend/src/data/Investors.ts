export interface Investor {
    id: string;
    name: string;
    type: string;
    country: string;
    commitmentAmount: number;
    commitmentCurrency: string;
}

export const investors: Investor[] = [
    {
        id: '1',
        name: 'Global Equity Partners',
        type: 'Institutional',
        country: 'UK',
        commitmentAmount: 5000000,
        commitmentCurrency: 'GBP',
    },
    {
        id: '2',
        name: 'Alpha Capital',
        type: 'Family Office',
        country: 'USA',
        commitmentAmount: 2500000,
        commitmentCurrency: 'USD',
    },
    {
        id: '3',
        name: 'Global Equity Partners',
        type: 'Institutional',
        country: 'UK',
        commitmentAmount: 5000000,
        commitmentCurrency: 'GBP',
    },
    {
        id: '4',
        name: 'Alpha Capital',
        type: 'Family Office',
        country: 'USA',
        commitmentAmount: 2500000,
        commitmentCurrency: 'USD',
    },
];

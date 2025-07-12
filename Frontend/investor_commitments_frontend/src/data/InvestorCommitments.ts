export interface InvestorCommitments {
    id: string;
    investorName: string;
    investorCountry: string;
    assetClass: string;
    amount: number;
    currency: string;
}

export const investorCommitments: InvestorCommitments[] = [
    {
        id: '1',
        investorName: 'Alpha Capital',
        investorCountry: 'USA',
        assetClass: 'Real Estate',
        amount: 5000000,
        currency: 'USD',
    },
    {
        id: '2',
        investorName: 'Beta Ventures',
        investorCountry: 'UK',
        assetClass: 'Private Equity',
        amount: 3200000,
        currency: 'GBP',
    },
    {
        id: '3',
        investorName: 'Omega Partners',
        investorCountry: 'Germany',
        assetClass: 'Infrastructure',
        amount: 2750000,
        currency: 'EUR',
    },
    {
        id: '4',
        investorName: 'Gamma Fund',
        investorCountry: 'Canada',
        assetClass: 'Hedge Fund',
        amount: 1500000,
        currency: 'CAD',
    },
    {
        id: '5',
        investorName: 'Delta Holdings',
        investorCountry: 'Australia',
        assetClass: 'Real Estate',
        amount: 4200000,
        currency: 'AUD',
    },
];

import type {InvestorCommitments} from "./InvestorCommitments.ts";

export interface GetInvestorCommitmentsResponse {
    investor_name: string;
    investor_commitments: InvestorCommitments[];
}

import { Table, Typography, Layout } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import { type InvestorCommitments} from '../../models/InvestorCommitments.ts';
import { type GetInvestorCommitmentsResponse} from "../../models/GetInvestorCommitmentsResponse.ts";
import {useEffect, useState} from "react";
import { Select } from 'antd';
import {useNavigate, useParams} from 'react-router-dom';
import { Button } from 'antd';
import axios from "axios";

const columns: ColumnsType<InvestorCommitments> = [
    {
        title: 'Asset Class',
        dataIndex: 'asset_class',
        key: 'asset_class',
    },
    {
        title: 'Amount',
        key: 'amount',
        render: (_, record) =>
            `${record.amount.toLocaleString()} ${record.currency}`,
    },
];

export default function CommitmentsPage() {

    const navigate = useNavigate();
    const [selectedAssetClasses, setSelectedAssetClasses] = useState<string[]>([]);
    const [investorName, setInvestorName] = useState("");
    const [commitments, setCommitments] = useState<InvestorCommitments[]>([]);
    const { id } = useParams();


    useEffect(() => {
        axios.get<GetInvestorCommitmentsResponse>(`http://localhost:5250/api/investor/${id}`)
            .then(response => {
                setInvestorName(response.data.investor_name);
                setCommitments(response.data.investor_commitments);

            })
            .catch(error => {
                console.error('Error fetching investors:', error);
            });
    }, [id]);

    const assetClassOptions = Array.from(
        new Set(commitments.map(c => c.asset_class))
    );


    const filteredCommitments = selectedAssetClasses.length > 0
        ? commitments.filter(c => selectedAssetClasses.includes(c.asset_class))
        : commitments;

    return (
        <Layout className={"layout-style"}>
                <Typography.Title level={2} style={{ textAlign: 'center' }}><i>{investorName}</i> Commitments</Typography.Title>
                <Select
                    className="asset-class-filter"
                    mode="multiple"
                    placeholder="Filter by Asset Class"
                    allowClear
                    value={selectedAssetClasses}
                    onChange={(values) => setSelectedAssetClasses(values)}
                >
                    {assetClassOptions.map(assetClass => (
                        <Select.Option key={assetClass} value={assetClass}>
                            {assetClass}
                        </Select.Option>
                    ))}
                </Select>
                <Table
                    columns={columns}
                    dataSource={filteredCommitments}
                    rowKey="id"
                    pagination={{
                        pageSize: 10,
                        showSizeChanger: false
                }}
                    className="commitments-table"
                    rowClassName={(_, index) =>
                        index % 2 === 0 ? 'even-row' : 'odd-row'
                    }
                />
                <Button
                    type="default"
                    onClick={() => navigate('/')}
                    style={{ marginBottom: 16 }}
                >
                    Back to Investors
                </Button>
        </Layout>
    );
}

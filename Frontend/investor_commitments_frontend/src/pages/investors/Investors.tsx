import { Table, Typography, Layout } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import { type Investor} from '../../models/Investor.ts';
import { type GetInvestorsResponse} from '../../models/GetInvestorsResponse.ts';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from 'antd';
import axios from 'axios';

const columns: ColumnsType<Investor> = [
    {
        title: 'Investor Name',
        dataIndex: 'name',
        key: 'name',
        responsive: ['xs', 'sm', 'md', 'lg', 'xl'],
    },
    {
        title: 'Investory Type',
        dataIndex: 'investory_type',
        key: 'investory_type',
        responsive: ['lg'],
    },
    {
        title: 'Country',
        dataIndex: 'country',
        key: 'country',
        responsive: ['lg'],
    },
    {
        title: 'Total Commitment',
        key: 'total_commitments',
        responsive: ['md'],
        render: (_, record) =>
            `${record.total_commitments.toLocaleString()} ${record.currency}`,
    },
];

export default function Investors() {
    const navigate = useNavigate();
    const [isMobile, setIsMobile] = useState(false);
    const [investors, setInvestors] = useState<Investor[]>([]);

    useEffect(() => {
        const handleResize = () => setIsMobile(window.innerWidth <= 991);
        handleResize();
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);

    useEffect(() => {
        axios.get<GetInvestorsResponse>('http://localhost:8080/api/investors')
            .then(response => {
                setInvestors(response.data.investors);
            })
            .catch(error => {
                console.error('Error fetching investors:', error);
            });
    }, []);

    const expandedRowRender = (record: Investor) => (
        <div className="expanded-details">
            <p><strong>Type:</strong> {record.investory_type}</p>
            <p><strong>Country:</strong> {record.country}</p>
            <p><strong>Commitment:</strong> {record.total_commitments.toLocaleString()} {record.currency}</p>
            <Button
                type="default"
                onClick={() => navigate(`/investor-commitments/${record.id}`)}
            >
                View Commitments
            </Button>
        </div>
    );

    return (
        <Layout className={"layout-style"}>
            <Typography.Title level={2} style={{ textAlign: 'center' }}>Investors</Typography.Title>
                <Table
                    columns={columns}
                    dataSource={investors}
                    rowKey="id"
                    expandable={
                        isMobile ? {
                            expandedRowRender,
                            expandRowByClick: true,
                        } : undefined}
                    pagination={{
                        pageSize: 10,
                        showSizeChanger: false
                    }}
                    className="investor-table"
                    rowClassName={(_, index) =>
                        index % 2 === 0 ? 'even-row' : 'odd-row'
                    }
                    onRow={
                        isMobile ? undefined :(record) => ({
                        onClick: () => navigate(`/investor-commitments/${record.id}`),
                        style: { cursor: 'pointer' },
                    })
                }
                />
            <p>NOTE: Click on investor to view Commitments</p>
        </Layout>
    );
}

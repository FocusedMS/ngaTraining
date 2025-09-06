import { useQuery } from '@tanstack/react-query'
import { api } from '../lib/api'
import { Link } from 'react-router-dom'

type Post = { id: number; title: string; slug: string; excerpt?: string }

export default function Home() {
  const { data, isFetching } = useQuery({
    queryKey: ['posts', { page:1, pageSize:10 }],
    queryFn: async () => (await api.get('/api/Posts', { params:{ page:1, pageSize:10 } })).data
  })

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold">Latest Posts</h1>
        {isFetching && <span className="text-sm text-gray-500">Refreshing…</span>}
      </div>
      <div className="grid md:grid-cols-2 gap-4">
        {data?.items?.map((p:Post)=>(
          <Link to={`/post/${p.slug}`} key={p.id} className="card p-4 hover:shadow-md transition">
            <div className="text-lg font-semibold">{p.title}</div>
            <p className="opacity-70">{p.excerpt}</p>
          </Link>
        ))}
        {!data?.items?.length && <div className="text-gray-500">No posts yet.</div>}
      </div>
    </div>
  )
}

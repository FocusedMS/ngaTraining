import { useForm } from 'react-hook-form'
import { z } from 'zod'
import { zodResolver } from '@hookform/resolvers/zod'
import { useDispatch, useSelector } from 'react-redux'
import { login } from '../store/slices/authSlice'
import { useNavigate } from 'react-router-dom'
import type { RootState } from '../store'

const schema = z.object({
  usernameOrEmail: z.string().min(3),
  password: z.string().min(6)
})
type Form = z.infer<typeof schema>

export default function Login() {
  const { register, handleSubmit, formState:{errors} } = useForm<Form>({ resolver: zodResolver(schema) })
  const nav = useNavigate()
  const dispatch = useDispatch()
  const loading = useSelector((s:RootState)=>s.auth.loading)

  const onSubmit = async (data: Form) => {
    await dispatch<any>(login(data))
    nav('/dashboard')
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="max-w-md mx-auto card p-6 space-y-3">
      <h1 className="text-xl font-semibold">Login</h1>
      <label className="label">Username or Email</label>
      <input className="input" placeholder="madhu@example.com" {...register('usernameOrEmail')} />
      {errors.usernameOrEmail && <p className="text-red-500 text-sm">{errors.usernameOrEmail.message}</p>}

      <label className="label">Password</label>
      <input className="input" type="password" placeholder="••••••••" {...register('password')} />
      {errors.password && <p className="text-red-500 text-sm">{errors.password.message}</p>}

      <button className="btn btn-primary" disabled={loading}>{loading ? 'Signing in…' : 'Sign in'}</button>

      <div className="text-sm opacity-70 mt-2">
        blogger: <code>madhu@example.com / Madhu@123</code><br/>
        admin: <code>admin / Admin@123</code>
      </div>
    </form>
  )
}

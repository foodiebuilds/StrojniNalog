import { describe, expect, it } from 'vitest'
import { parseOpravljeneUre } from './parseOpravljeneUre'

describe('parseOpravljeneUre', () => {
  it('sprejme decimalno vejico', () => {
    expect(parseOpravljeneUre('7,5')).toBe(7.5)
  })

  it('zavrne besedilo', () => {
    expect(parseOpravljeneUre('osem')).toBeNull()
  })
})

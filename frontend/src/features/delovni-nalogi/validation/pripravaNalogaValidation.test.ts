import { describe, expect, it } from 'vitest'
import { validirajPripravoNaloga } from './pripravaNalogaValidation'

describe('validirajPripravoNaloga', () => {
  it('zavrne konec pred začetkom', () => {
    const napake = validirajPripravoNaloga({
      strojId: '1',
      izvajalecId: '2',
      predvideniZacetek: '2026-12-01T10:00',
      predvideniKonec: '2026-12-01T08:00',
    })

    expect(napake.predvideniKonec).toBe('Predvideni konec mora biti po začetku.')
  })

  it('sprejme veljaven nalog', () => {
    const napake = validirajPripravoNaloga({
      strojId: '1',
      izvajalecId: '2',
      predvideniZacetek: '2026-12-01T08:00',
      predvideniKonec: '2026-12-01T16:00',
    })

    expect(napake).toEqual({})
  })
})

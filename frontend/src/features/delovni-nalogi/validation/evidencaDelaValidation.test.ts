import { describe, expect, it } from 'vitest'
import { preveriPotekZaOddajo, validirajEvidencoDela } from './evidencaDelaValidation'

describe('validirajEvidencoDela', () => {
  it('pri oddaji zahteva ure in opis', () => {
    const napake = validirajEvidencoDela({ opravljeneUre: '', opisIzvedbe: '' }, 'oddaj')

    expect(napake.opravljeneUre).toBe('Vnesite opravljene ure.')
    expect(napake.opisIzvedbe).toBe('Vnesite opis izvedbe.')
  })

  it('zavrne 0 ur', () => {
    const napake = validirajEvidencoDela({ opravljeneUre: '0', opisIzvedbe: 'Delo' }, 'shrani')

    expect(napake.opravljeneUre).toBe('Opravljene ure morajo biti večje od 0 in največ 1000.')
  })
})

describe('preveriPotekZaOddajo', () => {
  it('zahteva začetek in konec dela', () => {
    expect(preveriPotekZaOddajo({ dejanskiZacetek: null, dejanskiKonec: null })).not.toBeNull()
  })
})
